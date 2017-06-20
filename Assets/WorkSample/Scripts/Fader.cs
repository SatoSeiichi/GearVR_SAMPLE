using UnityEngine; 
using System.Collections; 
using System; 

public class Fader : MonoBehaviour {

	public enum FADE_TYPE 
	{ 
		FADE_IN = 0, 
		FADE_OUT = 1, 
		FADE_OUTIN = 2, 
	}; 

	private Texture2D	fadeTexture = null; 
	private float		fadeAlpha = 0; 

	protected void Start() 
	{ 
		// 黒テクスチャの作成。 
		this.fadeTexture = new Texture2D( 1, 1 ); 
		this.fadeTexture.SetPixel( 0, 0, Color.white); 
		this.fadeTexture.Apply(); 
	} 

	public void OnGUI() 
	{ 
		//透明度を更新して黒テクスチャを描画 
		if( Event.current.type == EventType.Repaint ) 
		{ 
			GUI.color = new Color( 255, 255, 255, this.fadeAlpha ); 
			GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), this.fadeTexture ); 
		} 
	} 

	// フェード開始 
	// _type フェードの種類 
	// _interval フェードにかける時間 
	// _interval _onActionフェード完了後に呼ぶ関数 
	public void StartFade( FADE_TYPE _type, float _interval, Action _onAction ) 
	{ 
		this.gameObject.SetActive( true ); 

		switch( _type ) 
		{ 
		case FADE_TYPE.FADE_OUT: 
			StartCoroutine( FadeOut( _interval, _onAction ) ); 
			break; 
		case FADE_TYPE.FADE_IN: 
			StartCoroutine( FadeIn( _interval, _onAction ) ); 
			break; 
		case FADE_TYPE.FADE_OUTIN: 
			StartCoroutine( FadeOutIn( _interval, _onAction ) ); 
			break; 
		}; 
	} 

	private IEnumerator Fade( FADE_TYPE _type, float interval ) 
	{ 
		float time = 0; 
		float min = ( _type == FADE_TYPE.FADE_OUT) ? 0.0f : 1.0f; 
		float max = ( _type == FADE_TYPE.FADE_OUT) ? 1.0f : 0.0f; 
		while( time <= interval ) 
		{ 
			this.fadeAlpha = Mathf.Lerp( min, max, time / interval ); 
			time += Time.deltaTime; 
			yield return 0; 
		} 
	} 

	private IEnumerator FadeOut( float interval, Action _onAction ) 
	{ 
		yield return StartCoroutine( Fade( FADE_TYPE.FADE_OUT, interval ) ); 

		_onAction(); 

	} 

	private IEnumerator FadeIn( float interval, Action _onAction ) 
	{ 
		yield return StartCoroutine( Fade( FADE_TYPE.FADE_IN, interval ) ); 

		_onAction(); 

	} 

	private IEnumerator FadeOutIn( float interval, Action _onAction ) 
	{ 
		yield return StartCoroutine( Fade( FADE_TYPE.FADE_OUT, interval ) ); 

		_onAction(); 

		yield return StartCoroutine( Fade( FADE_TYPE.FADE_IN, interval ) ); 

	} 
}
