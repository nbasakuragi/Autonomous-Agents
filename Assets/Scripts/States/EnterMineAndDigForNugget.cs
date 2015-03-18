﻿using System;
using Assets.Scripts.Agents;
using UnityEngine;

namespace Assets.Scripts.States
{
	/// <summary>
	/// Enter mine and Dig for Nugget
	/// </summary>
    public sealed class EnterMineAndDigForNugget<T> : State<T> where T : Miner
    {
		private EnterMineAndDigForNugget()
		{
			Enter += EnterMineAndDigForNugget_Enter;
			Exit += EnterMineAndDigForNugget_Exit;
		}


        #region [ Singleton Implementation ]
        public static EnterMineAndDigForNugget<T> Instance { get { return Nested.instance; } }

        /// This is a fully lazy initialization implementation
        /// Instantiation is triggered by the first reference to the static member of the nested class, 
        /// which only occurs in Instance. This means the implementation is fully lazy.
        /// Note that although nested classes have access to the enclosing class's private members, the reverse is not true, 
        /// hence the need for instance to be internal here. That doesn't raise any other problems, though, as the class itself is private. 
        /// The code is a bit more complicated in order to make the instantiation lazy, however.
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly EnterMineAndDigForNugget<T> instance = new EnterMineAndDigForNugget<T>();
        }
        #endregion

		void EnterMineAndDigForNugget_Enter(object sender, AgentEventArgs<T> e)
        {
            if (e.Agent.Location != LocationType.Goldmine)
			{
				Debug.Log("Walkin' to the goldmine: AgentId" + e.Agent.NextValidId);
				e.Agent.ChangeLocation(LocationType.Goldmine);
			}
        }
       
        void EnterMineAndDigForNugget_Exit(object sender, AgentEventArgs<T> e)
        {
			Debug.Log("Ah'm leavin' the gold mine with mah pockets full o' sweet gold");
        }

        public override void Execute(T agent)
        { 
			agent.AddGoldToInventory(1);

			agent.IncreaseFatigue();

			Debug.Log("Pickin' up a nugget");

			if (agent.IsPocketFull())
			{
				agent.ChangeState(VisitBankAndDepositGold<T>.Instance);
			}

			if (agent.IsThirsty())
			{
				agent.ChangeState(QuenchThirst<T>.Instance);
			}
        }
    }
}
