using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionControllerStateCache : MonoBehaviour
{ /*
    /// <summary> 
    /// Internal helper class which associates a Motion Controller 
    /// and its known state 
    /// </summary> 
    private class MotionControllerState
    {
        /// <summary> 
        /// Construction 
        /// </summary> 
        /// <param name="mc">motion controller</param>` 
        public MotionControllerState(MotionController mc)
        {
            this.MotionController = mc;
        }

        /// <summary> 
        /// Motion Controller that the state represents 
        /// </summary> 
        public MotionController MotionController { get; private set; } 
      
    }

    private MotionControllerWatcher _watcher;
    private Dictionary<Handedness, MotionControllerState>
        _controllers = new Dictionary<Handedness, MotionControllerState>();

    /// <summary> 
    /// Starts monitoring controller's connections and disconnections 
    /// </summary> 
    public void Start()
    {
        _watcher = new MotionControllerWatcher();
        _watcher.MotionControllerAdded += _watcher_MotionControllerAdded;
        _watcher.MotionControllerRemoved += _watcher_MotionControllerRemoved;
        var nowait = _watcher.StartAsync();
    }

    /// <summary> 
    /// Stops monitoring controller's connections and disconnections 
    /// </summary> 
    public void Stop()
    {
        if (_watcher != null)
        {
            _watcher.MotionControllerAdded -= _watcher_MotionControllerAdded;
            _watcher.MotionControllerRemoved -= _watcher_MotionControllerRemoved;
            _watcher.Stop();
        }
    }

    /// <summary> 
    /// called when a motion controller has been removed from the system: 
    /// Remove a motion controller from the cache 
    /// </summary> 
    /// <param name="sender">motion controller watcher</param> 
    /// <param name="e">motion controller </param> 
    private void _watcher_MotionControllerRemoved(object sender, MotionController e)
    {
        lock (_controllers)
        {
            _controllers.Remove(e.Handedness);
        }
    }

    /// <summary> 
    /// called when a motion controller has been added to the system: 
    /// Remove a motion controller from the cache 
    /// </summary> 
    /// <param name="sender">motion controller watcher</param> 
    /// <param name="e">motion controller </param> 
    private void _watcher_MotionControllerAdded(object sender, MotionController e)
    {
        lock (_controllers)
        {
            _controllers[e.Handedness] = new MotionControllerState(e);
        }
    }*/
}
