using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Commands : byte{
	NONE = 0,
	MOVETARGET = 1,
	MOVEAREA = 2,
	CANCEL = 3,
	ATTACKTARGET = 4,
	ATTACKAREA = 5,
	STOP = 6,
	HOLD = 7,
	ABILITY = 8,
}

public class UnitCommand {
	public Commands id = Commands.NONE;
}

public class MoveCommand : UnitCommand {
	public Vector2 target;
	public MoveCommand(){
		id = Commands.MOVETARGET;
	}
}

// public class AbilityCommand : UnitCommand {
// 	public Execute(ushort abilityId){
// 		switch(abilityId){
// 			case 1: 
// 			break;
// 		}
// 	}
// }

// public class AbilityPrototype {
// 	string castType = "target";
// 	int radius = 4;
// }