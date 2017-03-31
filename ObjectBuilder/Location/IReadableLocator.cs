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

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// �ɶ�ȡ�Ķ�λ��
    /// </summary>
    /// <remarks>
    /// <para>��λ����һ���ֵ�ļ�ֵ����������ֵ�������ã����Զ�λһ�����󲻱��������š�������뱣�ֶ�����ţ���Ӧ�ÿ���ʹ��<see cref="ILifetimeContainer"/>.</para>
    /// </remarks>
    public interface IReadableLocator : IEnumerable<KeyValuePair<object, object>>
    {
        /// <summary>
        /// ���ض�λ���е�����
        /// </summary>
        int Count { get; }

        /// <summary>
        /// ����λ��
        /// </summary>
        IReadableLocator ParentLocator { get; }

        /// <summary>
        /// �����λ����ֻ���ģ�����true
        /// </summary>
        bool ReadOnly { get; }

        /// <summary>
        /// ȷ����λ���Ƿ�����������Ķ���
        /// </summary>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <returns>����ö�λ���������ڸü��Ķ����򷵻�true�����򷵻�false</returns>
        /// <exception cref="ArgumentNullException">keyΪnullʱ</exception>
        bool Contains(object key);

        /// <summary>
        /// ȷ����λ���Ƿ�����������Ķ���
        /// </summary>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <param name="options">����ѡ��(һ��ö��)</param>
        /// <returns>����ö�λ���������ڸü��Ķ����򷵻�true�����򷵻�false</returns>
        /// <exception cref="ArgumentNullException">keyΪnullʱ</exception>
        /// <exception cref="ArgumentException">SearchModeѡ�����Чö��ֵ</exception>
        bool Contains(object key, SearchMode options);

        /// <summary>
        /// �Զ�������������������һ����ʱ����
        /// </summary>
        /// <param name="predicate">�Զ������������Ƿ����<see cref="IReadableLocator"/>����</param>
        /// <returns>����һ���µĶ�λ��</returns>
        /// <exception cref="ArgumentNullException">PredicateΪnull</exception>
        IReadableLocator FindBy(Predicate<KeyValuePair<object, object>> predicate);

        /// <summary>
        /// �Զ�������������������һ����ʱ����
        /// </summary>
        /// <param name="options">����ѡ��(һ��ö��)</param>
        /// <param name="predicate">�Զ������������Ƿ����<see cref="IReadableLocator"/>����</param>
        /// <returns>����һ���µĶ�λ��</returns>
        /// <exception cref="ArgumentNullException">PredicateΪnull</exception>
        /// <exception cref="ArgumentException">SearchModeѡ�����Чö��ֵ</exception>
        IReadableLocator FindBy(SearchMode options, Predicate<KeyValuePair<object, object>> predicate);

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        /// <typeparam name="TItem">Ҫ���ҵĶ��������</typeparam>
        /// <returns>����ҵ�����ֱ�ӷ��أ���ΪNULL</returns>
        TItem Get<TItem>();

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        /// <typeparam name="TItem">Ҫ���ҵĶ��������</typeparam>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <returns>����ҵ�����ֱ�ӷ��أ���ΪNULL</returns>
        /// <exception cref="ArgumentNullException">KeyΪnull.</exception>
        TItem Get<TItem>(object key);

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        /// <typeparam name="TItem">Ҫ���ҵĶ��������</typeparam>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <param name="options">����ѡ��(һ��ö��)</param>
        /// <returns>����ҵ�����ֱ�ӷ��أ���ΪNULL</returns>
        /// <exception cref="ArgumentNullException">KeyΪnull.</exception>
        /// <exception cref="ArgumentException">SearchModeѡ�����Чö��ֵ</exception>
        TItem Get<TItem>(object key, SearchMode options);

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <returns>����ҵ�����ֱ�ӷ��أ���ΪNULL</returns>
        /// <exception cref="ArgumentNullException">Key Ϊ null.</exception>
        object Get(object key);

        /// <summary>
        /// �Ӷ�λ���л�ȡһ������
        /// </summary>
        /// <param name="key">Key��Ψһ��ʶ</param>
        /// <param name="options">����ѡ��(һ��ö��)</param>
        /// <returns>����ҵ�����ֱ�ӷ��أ���ΪNULL</returns>
        /// <exception cref="ArgumentNullException">Key Ϊ null.</exception>
        /// <exception cref="ArgumentException">SearchModeѡ�����Чö��ֵ</exception>
        object Get(object key, SearchMode options);
    }
}