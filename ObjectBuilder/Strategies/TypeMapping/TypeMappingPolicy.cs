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
    /// ʵ�� <see cref="ITypeMappingPolicy"/>
    /// </summary>
    public class TypeMappingPolicy : ITypeMappingPolicy
    {
        private DependencyResolutionLocatorKey pair;

        /// <summary>
        /// ʵ���� <see cref="TypeMappingPolicy"/> ��
        /// </summary>
        /// <param name="type">���������������</param>
        /// <param name="id">�����������Ψһ��ʶ��</param>
        public TypeMappingPolicy(Type type, string id)
        {
            pair = new DependencyResolutionLocatorKey(type, id);
        }

        /// <summary>
        /// ӳ��һ��[����/ID]
        /// </summary>
        /// <param name="incomingTypeIDPair">������[��/ID]</param>
        /// <returns>�µ�[��/ID]</returns>
		public DependencyResolutionLocatorKey Map(DependencyResolutionLocatorKey incomingTypeIDPair)
        {
            return pair;
        }
    }
}
