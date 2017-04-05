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
    /// ����Ĳ���Ϊ <see cref="TypeMappingStrategy"/>�������� <see cref="IBuilderPolicy"/> ��ӳ�䴦��������ԣ�
    /// ÿһ�������Ķ����Ǵ洢��Locator�У�Locator������ÿһ���������������Type/ID��<see cref="ITypeMappingPolicy"/>���ڽ����Ҫ���������Ķ���ӳ�䵽��ȷ��Type/ID�У�
    /// </summary>
    public interface ITypeMappingPolicy : IBuilderPolicy
    {
        /// <summary>
        /// ��һ��<see cref="DependencyResolutionLocatorKey"/> ӳ�䵽��һ�� Key
        /// </summary>
        /// <param name="incomingTypeIDPair">������[��/ID]</param>
        /// <returns>�µ�[��/ID]</returns>
        DependencyResolutionLocatorKey Map(DependencyResolutionLocatorKey incomingTypeIDPair);
    }
}