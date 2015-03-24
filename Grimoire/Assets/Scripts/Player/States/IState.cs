﻿using UnityEngine;
using System.Collections;

/*========================================================
 * Author: Tyler Remazki
 *
 * Class : IState (Abstract Class)
 *
 * Description: Abstract class that all derived states use the functionality of.
 =========================================================*/

namespace PlayerStates
{
	public abstract class IState
	{
		protected PlayerFSM m_playerFSM;
		private float m_blockTimer;

		/// <summary>
		/// Set the FSM that contains this State so that we can have access to reference variables.
		/// </summary>
		/// <param name="_fsm">The FSM Reference.</param>
		public void SetFSM( PlayerFSM _fsm )
		{
			m_playerFSM = _fsm;
		}

		/// <summary>
		/// Block the state switch for X seconds.
		/// </summary>
		/// <param name="_time">Time to block the state switch for.</param>
		/// <returns></returns>
		public void BlockStateSwitch( float _time )
		{
			if ( !m_playerFSM.Blocking )
			{
				m_blockTimer = _time;
				m_playerFSM.Blocking = true;
			}
			else
			{
				m_blockTimer -= Time.deltaTime;
				if ( m_blockTimer <= 0.0f )
				{
					m_playerFSM.Blocking = false;
					m_blockTimer = 0.0f;
				}
			}
		}

		/// <summary>
		/// Add more time to the current state blocking. 
		/// </summary>
		/// <param name="_time">Time to be added.</param>
		public void AddBlockingTime( float _time)
		{
			m_blockTimer += _time;
		}

		/// <summary>
		/// Called when the current state in the FSM is switched to this state.
		/// </summary>
		public virtual void OnSwitch() { }

		/// <summary>
		/// Called when the current state in the FSM is switched from this state.
		/// </summary>
		public virtual void OnExit() { }

		/// <summary>
		/// Called when this state is the FSM's current state. 
		/// </summary>
		public abstract void ExecuteState();

		/// <summary>
		/// Receive the current FSM that this state is contained within.
		/// </summary>
		/// <returns></returns>
		protected PlayerFSM GetFSM() { return m_playerFSM; }
	}
}
