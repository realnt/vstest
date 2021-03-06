// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Base class for all test case event arguments.
    /// </summary>
#if NET46
        [Serializable] 
#endif
    public abstract class TestCaseEventArgs : DataCollectionEventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEventArgs"/> class. 
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case.
        /// </param>
        protected TestCaseEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase)
            : base(context)
        {
            this.TestCaseId = testCaseId;
            this.TestCaseName = testCaseName == null ? string.Empty : testCaseName;
            this.IsChildTestCase = isChildTestCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEventArgs"/> class. 
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testElement">
        /// The test element of the test that this event is for.
        /// </param>
        protected TestCaseEventArgs(
            DataCollectionContext context,
            TestCase testElement)
            : this(context, Guid.Empty, null, false)
        {
            // NOTE: ONLY USE FOR UNIT TESTING!
            //  This overload is only here for 3rd parties to use for unit testing
            //  their data collectors.  Internally we should not be passing the test element
            //  around in the events as this is extra information that needs to be seralized
            //  and the Execution Plugin Manager will fill this in for us before the event
            //  is sent to the data collector when running in a production environment.

            // todo
            // EqtAssert.ParameterNotNull(testElement, "testElement");

            this.TestElement = testElement;
            this.TestCaseId = testElement.Id;
            this.TestCaseName = testElement.DisplayName;
            // IsChildTestCase = testElement != null &&
            // !testElement.ParentExecId.Equals(TestExecId.Empty);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the test case ID
        /// </summary>
        public Guid TestCaseId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the test case name
        /// </summary>
        public string TestCaseName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this is a child test case, false if this is a top-level test case
        /// </summary>
        public bool IsChildTestCase
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets test element of the test this event is for.
        /// </summary>
        public TestCase TestElement
        {
            get;
            internal set;
        }

        #endregion
    }

    /// <summary>
    /// Test Case Start event arguments.
    /// </summary>
#if NET46
        [Serializable]
#endif
    public sealed class TestCaseStartEventArgs : TestCaseEventArgs
    {
        #region Constructor       

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseStartEventArgs"/> class. 
        /// Initializes the instance by storing the given information.
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testElement">
        /// The test element of the test that this event is for.
        /// </param>
        public TestCaseStartEventArgs(
            DataCollectionContext context,
            TestCase testElement)
            : base(context, testElement)
        {
            // NOTE: ONLY USE FOR UNIT TESTING!
            //  This overload is only here for 3rd parties to use for unit testing
            //  their data collectors.  Internally we should not be passing the test element
            //  around in the events as this is extra information that needs to be seralized
            //  and the Execution Plugin Manager will fill this in for us before the event
            //  is sent to the data collector when running in a production environment.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseStartEventArgs"/> class. 
        /// Initializes the instance by storing the given information
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case
        /// </param>
        internal TestCaseStartEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase)
            : base(context, testCaseId, testCaseName, isChildTestCase)
        {
            Debug.Assert(context.HasTestCase, "Context is not for a test case");
        }

        #endregion
    }

    /// <summary>
    /// Test Case End event arguments.
    /// </summary>
#if NET46
        [Serializable] 
#endif
    public sealed class TestCaseEndEventArgs : TestCaseEventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEndEventArgs"/> class. 
        /// Initializes the instance by storing the given information.
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testElement">
        /// The test element of the test that this event is for.
        /// </param>
        /// <param name="testOutcome">
        /// The outcome of the test case.
        /// </param>
        public TestCaseEndEventArgs(
            DataCollectionContext context,
            TestCase testElement,
            TestOutcome testOutcome)
            : base(context, testElement)
        {
            // NOTE: ONLY USE FOR UNIT TESTING!
            //  This overload is only here for 3rd parties to use for unit testing
            //  their data collectors.  Internally we should not be passing the test element
            //  around in the events as this is extra information that needs to be seralized
            //  and the Execution Plugin Manager will fill this in for us before the event
            //  is sent to the data collector when running in a production environment.
            this.TestOutcome = testOutcome;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEndEventArgs"/> class. 
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case
        /// </param>
        internal TestCaseEndEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase)
            : this(context, testCaseId, testCaseName, isChildTestCase, TestOutcome.Failed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEndEventArgs"/> class. 
        /// Initializes the instance by storing the given information
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case
        /// </param>
        /// <param name="testOutcome">
        /// The outcome of the test case.
        /// </param>
        internal TestCaseEndEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase,
            TestOutcome testOutcome)
            : base(context, testCaseId, testCaseName, isChildTestCase)
        {
            Debug.Assert(context.HasTestCase, "Context is not for a test case");
            this.TestOutcome = testOutcome;
        }        

        #endregion

        #region Properties

        /// <summary>
        /// Gets the outcome of the test.
        /// </summary>
        public TestOutcome TestOutcome
        {
            get;
            private set;
        }
        #endregion
    }

    /// <summary>
    /// Test Case Pause Event arguments.
    /// </summary>
#if NET46
    [Serializable]
#endif
    public sealed class TestCasePauseEventArgs : TestCaseEventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasePauseEventArgs"/> class. 
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testElement">
        /// The test element of the test that this event is for.
        /// </param>
        public TestCasePauseEventArgs(
            DataCollectionContext context,
            TestCase testElement)
            : base(context, testElement)
        {
            // NOTE: ONLY USE FOR UNIT TESTING!
            //  This overload is only here for 3rd parties to use for unit testing
            //  their data collectors.  Internally we should not be passing the test element
            //  around in the events as this is extra information that needs to be seralized
            //  and the Execution Plugin Manager will fill this in for us before the event
            //  is sent to the data collector when running in a production environment.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasePauseEventArgs"/> class. 
        /// Initializes the instance by storing the given information
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case
        /// </param>
        internal TestCasePauseEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase)
            : base(context, testCaseId, testCaseName, isChildTestCase)
        {
            Debug.Assert(context.HasTestCase, "Context is not for a test case");
        }

        #endregion
    }

    /// <summary>
    /// Test Case Resume Event arguments.
    /// </summary>
#if NET46
    [Serializable]
#endif
    public sealed class TestCaseResumeEventArgs : TestCaseEventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseResumeEventArgs"/> class. 
        /// Initializes the instance by storing the given information.
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testElement">
        /// The test element of the test that this event is for.
        /// </param>
        public TestCaseResumeEventArgs(
            DataCollectionContext context,
            TestCase testElement)
            : base(context, testElement)
        {
            // NOTE: ONLY USE FOR UNIT TESTING!
            //  This overload is only here for 3rd parties to use for unit testing
            //  their data collectors.  Internally we should not be passing the test element
            //  around in the events as this is extra information that needs to be seralized
            //  and the Execution Plugin Manager will fill this in for us before the event
            //  is sent to the data collector when running in a production environment.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseResumeEventArgs"/> class. 
        /// Initializes the instance by storing the given information
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case
        /// </param>
        internal TestCaseResumeEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase)
            : base(context, testCaseId, testCaseName, isChildTestCase)
        {
            Debug.Assert(context.HasTestCase, "Context is not for a test case");
        }       

        #endregion
    }

    /// <summary>
    /// Test Case Reset Event arguments.
    /// </summary>
#if NET46
    [Serializable] 
#endif
    public sealed class TestCaseResetEventArgs : TestCaseEventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseResetEventArgs"/> class. 
        /// Initializes the instance by storing the given information.
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testElement">
        /// The test element of the test that this event is for.
        /// </param>
        public TestCaseResetEventArgs(
            DataCollectionContext context,
            TestCase testElement)
            : base(context, testElement)
        {
            // NOTE: ONLY USE FOR UNIT TESTING!
            //  This overload is only here for 3rd parties to use for unit testing
            //  their data collectors.  Internally we should not be passing the test element
            //  around in the events as this is extra information that needs to be seralized
            //  and the Execution Plugin Manager will fill this in for us before the event
            //  is sent to the data collector when running in a production environment.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseResetEventArgs"/> class. 
        /// Initializes the instance by storing the given information
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case
        /// </param>
        internal TestCaseResetEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase)
            : base(context, testCaseId, testCaseName, isChildTestCase)
        {
            Debug.Assert(context.HasTestCase, "Context is not for a test case");
        }       

        #endregion
    }

    /// <summary>
    /// Test Case Failed Event arguments.
    /// </summary>
#if NET46
    [Serializable] 
#endif
    public sealed class TestCaseFailedEventArgs : TestCaseEventArgs
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseFailedEventArgs"/> class. 
        /// Initializes the instance by storing the given information.
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testElement">
        /// The test element of the test that this event is for.
        /// </param>
        /// <param name="failureType">
        /// The type of failure which has occurred.
        /// </param>
        public TestCaseFailedEventArgs(
            DataCollectionContext context,
            TestCase testElement,
            TestCaseFailureType failureType)
            : base(context, testElement)
        {
            // NOTE: ONLY USE FOR UNIT TESTING!
            //  This overload is only here for 3rd parties to use for unit testing
            //  their data collectors.  Internally we should not be passing the test element
            //  around in the events as this is extra information that needs to be seralized
            //  and the Execution Plugin Manager will fill this in for us before the event
            //  is sent to the data collector when running in a production environment.
            this.FailureType = failureType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseFailedEventArgs"/> class. 
        /// </summary>
        /// <param name="context">
        /// Context information for the test case
        /// </param>
        /// <param name="testCaseId">
        /// The test case ID
        /// </param>
        /// <param name="testCaseName">
        /// The test case name
        /// </param>
        /// <param name="isChildTestCase">
        /// True if this is a child test case, false if this is a top-level test case
        /// </param>
        /// <param name="failureType">
        /// The type of failure which has occurred.
        /// </param>
        internal TestCaseFailedEventArgs(
            DataCollectionContext context,
            Guid testCaseId,
            string testCaseName,
            bool isChildTestCase,
            TestCaseFailureType failureType)
            : base(context, testCaseId, testCaseName, isChildTestCase)
        {
            Debug.Assert(context.HasTestCase, "Context is not for a test case");

            if (failureType < TestCaseFailureType.None || failureType > TestCaseFailureType.Other)
            {
                throw new ArgumentOutOfRangeException(nameof(failureType));
            }

            this.FailureType = failureType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of failure which occurred.
        /// </summary>
        public TestCaseFailureType FailureType { get; private set; }

        #endregion
    }
}
