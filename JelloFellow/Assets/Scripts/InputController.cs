﻿using UnityEngine;

/// <inheritdoc />
/// <summary>
/// Recognizes different controllers, and sees if they are supported.
/// </summary>
public class InputController : Singleton<InputController> {
  private Input2D input;
  private InputControllerInfo info;
  
  private void Awake() {    
    /* default values */
		input = gameObject.AddComponent<SimpleInput>();
    info = null;
    
    /* get the valid controller in the joystick names */
    foreach (string controller in Input.GetJoystickNames()) {
      string lower_controller = controller.ToLower();

      /* xbox support */
      if (lower_controller.Contains("xbox") || lower_controller.Contains("microsoft")) {
        info = gameObject.AddComponent<XBoxOneControllerInfo>();
        break;
      } 
      
      /* ps4 support */
      if(lower_controller.Contains("sony")) {
        info = gameObject.AddComponent<Ps4ControllerInfo>();
        break;
      }
    }

    if (info != null) {
      /* output controller type */
      Debug.Log("Controller: " + info.controller_type());
      input.Init(info);
    } else {
      input = gameObject.AddComponent<ManualInput>();
      Debug.LogWarning("Please plugin a valid controller, and restart the game. Manual control has been given.");
    }
    
    /* instantiate other known singletons if must past this point */
    AudioManager _audio_manager = AudioManager.instance;
    GameController _game_controller = GameController.instance;
    MainScript _main_script = MainScript.instance;
  }
  
  /// <summary>
  /// Grabs the auto-assigned input based on the controller.
  /// </summary>
  /// <returns>Auto-assigned input.</returns>
  public Input2D GetInput() {
    return input;
  }

  public ControllerType type() {
    if (info == null) {
      if (input.GetType() == typeof(ManualInput)) {
        return ControllerType.MANUAL;
      }
    } else {
      if(info.GetType() == typeof(Ps4ControllerInfo)) {
        return ControllerType.PS4;
      } else if(info.GetType() == typeof(XBoxOneControllerInfo)) {
        return ControllerType.XBOXONE;
      }
    }

    return ControllerType.UNKNOWN;
  }
}

public enum ControllerType { MANUAL, PS4, XBOXONE, UNKNOWN }
