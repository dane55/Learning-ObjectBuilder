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
    /// ������ <see cref="IBuilderStrategy"/> ������������ָ�ĵ������ͬ����������������������ǳ䵱��·����
    /// ���鿴��ǰ�Ķ�λ�����Ƿ��Ѿ�����Ҫ�����Ķ�������У����ͰѶ��󷵻أ��������ѿ���Ȩ�ƽ�����һ�����ԡ�
    /// 
    /// </summary>
    public class SingletonStrategy : BuilderStrategy
    {
        /// <summary>
        /// ���ڹ����������������е���
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="existing">һ��Ĭ�ϴ�null���󴴽��������������д���һ���µĶ���ʵ���������Ϊnull�����������������ж���</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>�����Ķ���</returns>
		public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            DependencyResolutionLocatorKey key = new DependencyResolutionLocatorKey(typeToBuild, idToBuild);  //
            //DependencyResolutionLocatorKey����ıȽϵ��õ���Equals�������������������ͬ����id���ʱ��Ĭ���������
            if (context.Locator != null && context.Locator.Contains(key, SearchMode.Local)) //
            {
                //��ȡDependencyResolutionLocatorKey���󣬼��Locator��Ϊ�ղ����� Locator ���ڸö�����ôֱ�ӷ��أ�����ִ����һ������
                TraceBuildUp(context, typeToBuild, idToBuild, "");
                return context.Locator.Get(key);
            }
            //
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }
    }
}