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
    /// ����һ�����ԡ�������Ҫͬʱ֧�ֽ��������٣�������Ҫͬʱ֧�ֽ����Ͳ������Ȼ�����ֱ��ʵ������ӿڣ���Ҳ����ѡ��ʹ��<see cref="BuilderStrategy"/> ��Ϊ���࣬��Ĳ��ԣ���Ϊ�����ṩ�����õĸ���������ʹ��֧�ֽ����Ͳ�ж
    /// </summary>
    public interface IBuilderStrategy
    {
        /// <summary>
        /// ���ڹ����������������е���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="existing">һ��Ĭ�ϴ�null���󴴽��������������д���һ���µĶ���ʵ���������Ϊnull�����������������ж���</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>�����Ķ���</returns>
        object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild);

        /// <summary>
        /// ���ڴݻٶ������������е���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="item">��Ҫ���ٵĶ���ʵ��</param>
        /// <returns>���ص�ǰ���ٵĶ���ʵ��</returns>
        object TearDown(IBuilderContext context, object item);
    }
}