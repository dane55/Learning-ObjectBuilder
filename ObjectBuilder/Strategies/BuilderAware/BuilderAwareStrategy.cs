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
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ������ <see cref="BuilderStrategy"/> ��<see cref="BuilderAwareStrategy"/>�����ǳ�ʼ����ɽ׶����һ��ȱʡ�Ĳ��ԣ�
    /// ��������֪����ʵ������һ���ص����ԣ�һ��IBuiderAware�Ľӿڱ�OB�ṩ���κ�ʵ����IBuiderAware�ӿڵĶ���
    /// ������׶λ�õ�һ��OnBuilltUp���¼�֪ͨ��ͬʱ�ڶ���ж�ص�ʱ���õ�OnTearingDown��֪ͨ������֪ͨ�¼�����BuilderAwareStrategy�Ĺ���
    /// </summary>
    public class BuilderAwareStrategy : BuilderStrategy
    {
        /// <summary>
        /// See <see cref="IBuilderStrategy.BuildUp"/> for more information.
        /// </summary>
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            IBuilderAware awareObject = existing as IBuilderAware;

            if (awareObject != null)
            {
                TraceBuildUp(context, t, id, Properties.Resources.CallingOnBuiltUp);
                awareObject.OnBuiltUp(id);
            }

            return base.BuildUp(context, t, existing, id);
        }

        /// <summary>
        /// See <see cref="IBuilderStrategy.TearDown"/> for more information.
        /// </summary>
        public override object TearDown(IBuilderContext context, object item)
        {
            IBuilderAware awareObject = item as IBuilderAware;

            if (awareObject != null)
            {
                TraceTearDown(context, item, Properties.Resources.CallingOnTearingDown);
                awareObject.OnTearingDown();
            }

            return base.TearDown(context, item);
        }
    }
}
