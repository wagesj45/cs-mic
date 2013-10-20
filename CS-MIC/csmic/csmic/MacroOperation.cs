using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csmic
{
    /// <summary>
    /// Represents the operation types supported by a scripted macro.
    /// </summary>
    internal enum OperationType
    {
        /// <summary>
        /// Represents a conditional block.
        /// </summary>
        If,
        /// <summary>
        /// Represents a conditional else block.
        /// </summary>
        Else,
        /// <summary>
        /// Represents a complete conditional block.
        /// </summary>
        IfElse,
        /// <summary>
        /// A while block.
        /// </summary>
        While,
        /// <summary>
        /// A for block.
        /// </summary>
        For,
        /// <summary>
        /// A function declaration.
        /// </summary>
        FunctionDeclaration,
        /// <summary>
        /// An echo statement.
        /// </summary>
        Echo,
        /// <summary>
        /// A say statement.
        /// </summary>
        Say,
        /// <summary>
        /// A display statement.
        /// </summary>
        Display,
        /// <summary>
        /// A statement to execute.
        /// </summary>
        Statement,
        /// <summary>
        /// A string to display.
        /// </summary>
        String,
        /// <summary>
        /// An unknown or malformed block.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// An operation object that executes a specified action.
    /// </summary>
    internal class MacroOperation
    {
        #region Members

        /// <summary>
        /// The type of operation represented by the operation.
        /// </summary>
        private OperationType operationType;
        /// <summary>
        /// The collection of children nodes that belong to the operation.
        /// </summary>
        private List<MacroOperation> children;
        /// <summary>
        /// A list of the necesary input to execute the operation.
        /// </summary>
        private List<string> input;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new macro operation node.
        /// </summary>
        /// <param name="operationType">The type of operation the node represents.</param>
        public MacroOperation(OperationType operationType)
        {
            this.operationType = operationType;
            this.children = new List<MacroOperation>();
            this.input = new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the children nodes of the operation.
        /// </summary>
        public List<MacroOperation> Children
        {
            get
            {
                return this.children;
            }
            set
            {
                this.children = value;
            }
        }

        /// <summary>
        /// Gets or sets the input for the operation.
        /// </summary>
        public List<string> Input
        {
            get
            {
                return this.input;
            }
            set
            {
                this.input = value;
            }
        }

        /// <summary>
        /// Gets or sets the operation type for this operation.
        /// </summary>
        public OperationType OperationType
        {
            get
            {
                return this.operationType;
            }
            set
            {
                this.operationType = value;
            }
        }

        #endregion
    }
}
