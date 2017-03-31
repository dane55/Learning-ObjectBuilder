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

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ������<see cref="IBuilderPolicy"/>�Ľӿڣ���������(������<see cref="IBuilderPolicy"/>�ӿڵĲ���Ϊ�˱�������ҽ����Ϊ��������)
    /// </summary>
    public interface ISingletonPolicy : IBuilderPolicy
    {
        /// <summary>
        /// �������Ӧ���ǵ���������true
        /// </summary>
        bool IsSingleton { get; }
    }
}