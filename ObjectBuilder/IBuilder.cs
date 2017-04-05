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
    /// ���󹹽��������󴴽���ܵ����ӿ�
    /// </summary>
    /// <typeparam name="TStageEnum">ObjectBuilder��������ʵ���Ĳ��Ե�ִ��˳��</typeparam>
    public interface IBuilder<TStageEnum>
    {
        /// <summary>
        /// ��������ʱ��������ʱ����/����(�������ԡ����캯���������ȣ���Ȼ��Щ����ͨ��ע��ķ�ʽȥ�������󣬵������������Ҫ�ṩ��IBuilder��Ȼ���ʹ���ߴ���һ�����Ϲ���Ķ���ʵ��)
        /// </summary>
        PolicyList Policies { get; }

        /// <summary>
        /// ����������ͷŶ�������Ĳ���(�����һ�����Եļ��ϣ����еĲ������һ��������������ͨ����������������)
        /// </summary>
        StrategyList<TStageEnum> Strategies { get; }


        /// <summary>
        /// ִ�д����������
        /// </summary>
        /// <param name="locator">���ɶ���Ķ�λ�����������Ѵ���ʱֱ���ڶ�λ���л�ȡ</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <param name="existing">�Ѵ��ڵĶ��������null������CreationStrategy������</param>
        /// <param name="transientPolicies">��ʱ������������</param>
        /// <returns>�����Ķ���</returns>
        object BuildUp(IReadWriteLocator locator, Type typeToBuild, string idToBuild, object existing, params PolicyList[] transientPolicies);


        /// <summary>
        /// ִ�д����������
        /// </summary>
        /// <typeparam name="TTypeToBuild">���󴴽��ķ������ͣ���Ҫ�����Ķ��������</typeparam>
        /// <param name="locator">���ɶ���Ķ�λ�����������Ѵ���ʱֱ���ڶ�λ���л�ȡ</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <param name="existing">�Ѵ��ڵĶ��������null������CreationStrategy������</param>
        /// <param name="transientPolicies">��ʱ������������</param>
        /// <returns>�����Ķ���</returns>
        TTypeToBuild BuildUp<TTypeToBuild>(IReadWriteLocator locator, string idToBuild, object existing, params PolicyList[] transientPolicies);

        /// <summary>
        /// ִ�ж�������ٲ���
        /// </summary>
        /// <typeparam name="TItem">���󴴽��ķ������ͣ���Ҫ���ٵĶ��������</typeparam>
        /// <param name="locator">���ɶ���Ķ�λ�����������Ѵ���ʱֱ���ڶ�λ���л�ȡ</param>
        /// <param name="item">��Ҫ���ٵĶ���ʵ��</param>
        /// <returns>���ص�ǰ���ٵĶ���ʵ��</returns>
        TItem TearDown<TItem>(IReadWriteLocator locator, TItem item);
    }
}