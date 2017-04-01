//===============================================================================
// Microsoft patterns & practices
// ObjectBuilder Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;

namespace Microsoft.Practices.ObjectBuilder
{
	/// <summary>
	/// Attribute applied to properties and constructor parameters, to describe when the dependency
	/// injection system should inject an object.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class DependencyAttribute : ParameterAttribute
	{
		private string name;
		private Type createType;
		private NotPresentBehavior notPresentBehavior = NotPresentBehavior.CreateNew;
		private SearchMode searchMode;

		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
		/// </summary>
		public DependencyAttribute()
		{
		}

        /// <summary>
        /// ��Ҫע��Ķ������ơ���ѡ
        /// </summary>
        public string Name
		{
			get { return name; }
			set { name = value; }
		}

        /// <summary>
        /// �������Ķ���û���ҵ������ָ������һ���µ���������ָ���������͡���ѡ
        /// </summary>
        public Type CreateType
		{
			get { return createType; }
			set { createType = value; }
		}

        /// <summary>
        /// ָ���ڶ�λ�������������ģʽ
        /// </summary>
        public SearchMode SearchMode
		{
			get { return searchMode; }
			set { searchMode = value; }
		}


        /// <summary>
        /// ��������Ϊ�ҵ�ʱ����Ϊ��Ĭ��Ϊ CreateNew<see cref="Microsoft.Practices.ObjectBuilder.NotPresentBehavior.CreateNew"/>.
        /// </summary>
        public NotPresentBehavior NotPresentBehavior
		{
			get { return notPresentBehavior; }
			set { notPresentBehavior = value; }
		}

        /// <summary>
        /// ������Ҫ�Ĳ������� <see cref="ParameterAttribute.CreateParameter"/> �в鿴����
        /// </summary>
        public override IParameter CreateParameter(Type annotatedMemberType)
		{
			return new DependencyParameter(annotatedMemberType, name, createType, notPresentBehavior, searchMode);
		}
	}
}
