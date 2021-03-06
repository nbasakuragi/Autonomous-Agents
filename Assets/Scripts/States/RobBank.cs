﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Agents;

namespace Assets.Scripts.States
{
    public class RobBank<T> : State where T : Bandit
    {
        private RobBank()
        {
            Enter += RobBank_Enter;
        }

       

        #region [ Singleton Implementation ]

        public static RobBank<T> Instance { get { return Nested.instance; } }

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

            internal static readonly RobBank<T> instance = new RobBank<T>();
        }
        #endregion

        private void RobBank_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.TargetLocation = LocationType.Bank;

            if (e.Agent.Location != LocationType.Bank)
                e.Agent.ChangeState<T>(WalkingTo<T>.Instance);
        }

        public override void Execute(Agent agent)
        {
            agent.Say("YAY a lot of good miner's gold!");
        }
    }
}
