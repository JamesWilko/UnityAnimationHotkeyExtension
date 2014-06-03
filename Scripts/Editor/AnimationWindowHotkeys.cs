using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Reflection;

[ExecuteInEditMode]
public class AnimationWindowHotkeys : Editor {

	static System.Type animationWindowType = null;
	static bool playingAnimation = false;

	static System.Type GetAnimationWindowType() {
		if( animationWindowType == null ){
			animationWindowType = System.Type.GetType( "UnityEditor.AnimationWindow,UnityEditor" );
		}
		return animationWindowType;
	}

	static Object GetOpenAnimationWindow() {
		Object[] openAnimationWindows = Resources.FindObjectsOfTypeAll( GetAnimationWindowType() );
		if ( openAnimationWindows.Length > 0 ) {
			return openAnimationWindows[0];
		}
		return null;
	}

	static void RepaintOpenAnimationWindow() {
		Object w = GetOpenAnimationWindow();
		if ( w != null ) {
			( w as EditorWindow ).Repaint();
		}
	}

	static void RunMethod( string method ) {
		Object w = GetOpenAnimationWindow();
		if ( w != null ) {
			GetAnimationWindowType().InvokeMember( method, BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, w, null );
			RepaintOpenAnimationWindow();
		}
	}

	[MenuItem( "Window/Extension/Animation/Play Toggle _p" )]
	static void PlayAnimation() {
		if ( playingAnimation ) {
			RunMethod( "Stop" );
		} else {
			RunMethod( "Play" );
		}
		playingAnimation = !playingAnimation;
	}

	[MenuItem( "Window/Extension/Animation/Prev Keyframe _i" )]
	static void PrevKeyframe() {
		RunMethod( "Prev" );
	}

	[MenuItem( "Window/Extension/Animation/Next Keyframe _o" )]
	static void NextKeyframe() {
		RunMethod( "Next" );
	}

}
