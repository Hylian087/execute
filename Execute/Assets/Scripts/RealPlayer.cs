using UnityEngine;
using System.Collections;

public class RealPlayer : Player {
	
	private Joypad joypad;
	
	public RealPlayer(Joypad joypad) {
		this.joypad = joypad;
	}
	
}
