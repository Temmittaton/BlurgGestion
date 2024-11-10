using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    private float MOVE_TRESHOLD = .1f;
    private Touch prevTouch;
    public State state;
    private Vector2 anchor, cameraTargetPos;
    public int selectedBuilding;
    public float cameraSpeed, camT;

    private Vector2 TouchPosToWorldPos (Vector2 touchPos) {
        Vector3 _pos = Camera.main.ScreenToWorldPoint (touchPos);
        return new Vector2 (_pos.x, _pos.y);
    }
    public void Frame () {
        if (Input.touchCount != 0) {
            Touch touch = Input.GetTouch (0);
            if (prevTouch.position == Vector2.down) {
                // New touch
                anchor = touch.position / new Vector2 (Screen.width, Screen.height);
            }
            else {
                // Movement or maintained touch
                Vector2 move = anchor - (touch.position / new Vector2 (Screen.width, Screen.height));
                bool isMoving = (move.magnitude > MOVE_TRESHOLD);

                switch (state) {
                    case (State.Free):
                        // Starting to move or staying still
                        if (!isMoving) { break; }
                        
                        cameraTargetPos += move * cameraSpeed * Time.deltaTime;
                        camT = 0;
                        state = State.MovingCamera;
                        break;
                    case (State.MovingCamera):
                        // Moving or staying still
                        if (!isMoving) { break; }

                        cameraTargetPos += move * cameraSpeed * Time.deltaTime;
                        camT = 0;
                        break;
                    case (State.MovingBuilding):
                        if (!isMoving) { break; }

                        GameManager.I.bM.MoveBuilding (move * Time.deltaTime);
                        break;
                    case (State.BuildingSelected):
                        if (!isMoving) { break; }

                        GameManager.I.bM.MoveBuilding (move * Time.deltaTime);
                        state = State.MovingBuilding;
                        break;
                }
            }

            prevTouch = touch;

            // Moving camera
            camT = Mathf.Max (1f, camT + Time.deltaTime);

            Vector3 _camPos = Camera.main.transform.position;
            Vector3 _newPos = Vector3.Lerp (_camPos, cameraTargetPos, Mathf.SmoothStep (0f, 1f, camT));
            Camera.main.transform.position = _newPos;
        }
        else if (prevTouch.position != Vector2.down) {
            // If not touching screen
            switch (state) {
                case (State.MovingCamera) :
                    state = State.Free;
                    break;
                case (State.MovingBuilding):
                    state = State.BuildingSelected;
                    break;
                case (State.Free):
                    int _selectedBuilding = GameManager.I.bM.SelectBuilding (TouchPosToWorldPos (prevTouch.position));
                    if (_selectedBuilding == -1) {
                        // Did not touch building
                        state = State.Free;
                    }
                    else if (_selectedBuilding == selectedBuilding) {
                        // Unselected building
                        selectedBuilding = -1;
                        state = State.Free;
                    }
                    else {
                        // Selected building
                        selectedBuilding = _selectedBuilding;
                        state = State.BuildingSelected;
                    }
                    break;
            }

            anchor = Vector2.down;
            prevTouch.position = Vector2.down;
        }
    }
}

public enum State {
    Free,
    MovingCamera,
    BuildingSelected,
    MovingBuilding,
}