﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIDispatcher.iOS.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a dispatcher that performs actions on the UI thread.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __IOS__
namespace MADE.App.Views.Threading
{
    using System;
    using System.Threading.Tasks;

    using Foundation;

    /// <summary>
    /// Defines a dispatcher that performs actions on the UI thread.
    /// </summary>
    public class UIDispatcher : IUIDispatcher
    {
        /// <summary>
        /// Gets the platform specified UI object to use as a reference.
        /// </summary>
        public object Reference { get; private set; }

        private NSObject NSObject => this.Reference as NSObject;

        /// <summary>
        /// Sets the <see cref="Foundation.NSObject"/> to use as a reference for dispatching actions on the UI thread.
        /// </summary>
        /// <param name="reference">
        /// The <see cref="Foundation.NSObject"/> object.
        /// </param>
        public void SetReference(object reference)
        {
            this.Reference = null;

            if (!(reference is NSObject activity))
            {
                return;
            }

            this.Reference = activity;
        }

        /// <summary>
        /// Schedules the provided action on the UI thread from a worker thread.
        /// </summary>
        /// <param name="action">
        /// The action to run on the dispatcher.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        public async Task RunAsync(Action action)
        {
            UIDispatcher dispatcher = this;
            dispatcher.CheckInitialized();
            if (action == null)
            {
                return;
            }

            await Task.Run(() => { this.NSObject?.InvokeOnMainThread(action); });
        }

        private void CheckInitialized()
        {
            if (this.Reference == null)
            {
                throw new InvalidOperationException(
                    "Cannot run an action as the NSObject has not be set as the reference in the SetReference method.");
            }
        }
    }
}
#endif