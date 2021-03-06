﻿using System;
using Assets.Scripts.Agents;

namespace Assets.Scripts.States
{
    public class PatrolTown<T> : State where T : Sheriff
    {
        #region [ Singleton Implementation ]
        public static PatrolTown<T> Instance { get { return Nested.instance; } }

        private PatrolTown()
        {
            Enter += PatrolTown_Enter;
        }



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

            internal static readonly PatrolTown<T> instance = new PatrolTown<T>();
        }
        #endregion

        private LocationType GetNextLocation()
        {
            var values = Enum.GetValues(typeof(LocationType));
            var random = new Random();
            var randomLocation = (LocationType)values.GetValue(random.Next(values.Length));

            return randomLocation;
        }

        private void PatrolTown_Enter(object sender, AgentEventArgs<Agent> e)
        {
           
            if (e.Agent.Location != e.Agent.TargetLocation)
                e.Agent.ChangeState<T>(WalkingTo<T>.Instance);
 
            e.Agent.Say(string.Format("I'ma check {0}",e.Agent.TargetLocation));
        }
        public override void Execute(Agent agent)
        {
            agent.TargetLocation = GetNextLocation();

            agent.ChangeState<T>(WalkingTo<T>.Instance);
        }
    }
}
