using System;
//using System.Runtime.Remoting.Messaging;

namespace CabalsCorner.Utilities
{
	/// <summary>
	/// Provides methods that manage the synchronous and asynchronous invocation of delegates.
	/// </summary>
	public class EventDispatcher
	{
		#region Events/Delegates

		private delegate void AsyncFireHandler(Delegate del, object[] args); 

		#endregion

		#region Class Operations

		/// <summary>
		/// Represents the callback method associated with a AsyncFireHandler, which is invoked from 
		/// EventDispatcher.AsyncExecute.
		/// </summary>
		/// <remarks>
		/// The OneWayAttribute class is used "...because the client of EventDispatcher does not care 
		/// about the result of publishing the event." (Programming .NET Components, 143)
		/// </remarks>
		/// <param name="del">The Delegate being invoked asynchronously.</param>
		/// <param name="args">>Variable number of arguments to be passed to the asynchronous Delegate invokation.</param>
		//[OneWay]
		private static void InvokeDelegate(Delegate del, object[] args)
		{
			del.DynamicInvoke(args);
		}

		public static void ClearInvocationList(Delegate del, Delegate del2)
		{
			if (del == null)
			{
				return;
			}

			Delegate.RemoveAll(del, del2);
		}

		/// <summary>
		/// Synchronously and defensively fires any type of event, passing any argument collection.  Any exceptions 
		/// thrown by the sink are written published in the event log.
		/// </summary>
		/// <remarks>
		/// This class is a simple implementation of the Command design pattern.  The "Invoke" operation is akin
		/// to the Command pattern's "Execute" method.
		/// </remarks>
		/// <param name="del">The Delegate that is invoked.</param>
		/// <param name="args">Variable number of arguments to be passed to Delegate invokation.</param>
		public static void SyncExecute(Delegate del, params object[] args)
		{
			if (del == null)
			{
				return;
			}

			Delegate[] delegates = del.GetInvocationList();
			foreach (Delegate sink in delegates)
			{
				try
				{
					sink.DynamicInvoke(args);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		/// <summary>
		/// The AsyncExecute() "...method iterates over the internal collection of the delegate 
		/// passed in. For each delegate in the list, it uses another delegate of type AsyncFire[Handler] to 
		/// asynchronously call the private helper method InvokeDelegate().  InvokeDelegate() simply 
		/// uses the DynamicInvoke() method of the Delegate type to invoke the delegate." 
		/// (Programming .NET Components, 139)
		/// </summary>
		/// <param name="del">The Delegate whose Delegate sinks are invoked asynchronously.</param>
		/// <param name="args">Variable number of arguments to be passed to the asynchronous Delegate invokation.</param>
		public static void AsyncExecute(Delegate del, params object[] args)
		{
			if (del == null)
			{
				return;
			}

			Delegate[] delegates = del.GetInvocationList();
			AsyncFireHandler asyncDel = null;
			foreach (Delegate sink in delegates)
			{
				asyncDel = new AsyncFireHandler(EventDispatcher.InvokeDelegate);
				asyncDel.BeginInvoke(sink, args, null, null);
			}
		} 

		#endregion

	} // class FireEvent

} // namespace DukeLibrary.Utilities