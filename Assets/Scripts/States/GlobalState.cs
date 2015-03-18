namespace Assets.Scripts.States
{
using System;
	
	public class GlobalState<T> : State<T>
	{
		private GlobalState ()
		{
		 
		}
		
		#region [ Singleton Implementation ]
		public static GlobalState<T> Instance { get { return Nested.instance; } }
		
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
			
			internal static readonly GlobalState<T> instance = new GlobalState<T>();
		}
		#endregion

		#region implemented abstract members of State

		public override void Execute (T agent)
		{ 

		}

		#endregion
	}
}

