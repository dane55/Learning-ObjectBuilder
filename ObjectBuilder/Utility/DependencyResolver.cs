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
using System.Globalization;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ��������������<see cref="DependencyResolver"/>�౻�����������������ֵ�Ļ�ȡ����
    /// </summary>
    public class DependencyResolver
    {
        IBuilderContext context;

        /// <summary>
        /// DependencyResolver���캯��.
        /// </summary>
        /// <param name="context">���󴴽�����������</param>
        public DependencyResolver(IBuilderContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeToResolve"></param>
        /// <param name="typeToCreate"></param>
        /// <param name="id"></param>
        /// <param name="notPresent"></param>
        /// <param name="searchMode"></param>
        /// <returns></returns>
        public object Resolve(Type typeToResolve, Type typeToCreate, string id, NotPresentBehavior notPresent, SearchMode searchMode)
        {
            if (typeToResolve == null) throw new ArgumentNullException("typeToResolve");
            if (!Enum.IsDefined(typeof(NotPresentBehavior), notPresent)) throw new ArgumentException(Properties.Resources.InvalidEnumerationValue, "notPresent");
            //�������������δָ������Ĭ��ΪҪ��������Ĳ������͡�
            if (typeToCreate == null)
            {
                typeToCreate = typeToResolve;
            }
            //��ȡ������Key
            DependencyResolutionLocatorKey key = new DependencyResolutionLocatorKey(typeToResolve, id);
            //��������������ҵ����򷵻ء�
            if (context.Locator.Contains(key, searchMode))
            {
                return context.Locator.Get(key, searchMode);
            }
            switch (notPresent)
            {
                //������ʱ�����Ǵ�����
                case NotPresentBehavior.CreateNew:
                    return context.HeadOfChain.BuildUp(context, typeToCreate, null, key.ID);
                //������ʱ������null��
                case NotPresentBehavior.ReturnNull:
                    return null;
                //�׳��쳣��
                default:
                    throw new DependencyMissingException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.DependencyMissing, typeToResolve.ToString()));
            }
        }
    }
}
