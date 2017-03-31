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
using NUnit.Framework;

namespace Microsoft.Practices.ObjectBuilder
{
	[TestFixture]
	public class LocatorFixture
	{
		#region Invalid parameter tests

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullKeyOnAddThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.Add(null, 1);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullValueOnAddThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.Add(1, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullKeyOnContainsThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.Contains(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullKeyOnGetThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.Get(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullKeyOnRemoveThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.Remove(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullPredicateOnFindByThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.FindBy(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void BadSearchModeOnContainsThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.Contains(1, (SearchMode)254);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void BadSearchModeOnGetThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.Get(1, (SearchMode)254);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void BadSearchModeOnFindByThrows()
		{
			IReadWriteLocator locator = new Locator();

			locator.FindBy((SearchMode)254, delegate { return true; });
		}

		#endregion

		[Test]
		public void LocatorIsNotReadOnly()
		{
			IReadWriteLocator locator = new Locator();

			Assert.IsFalse(locator.ReadOnly);
		}

		[Test]
		public void CanRegisterObjectByType()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add(typeof(object), o);
			Assert.IsNotNull(locator.Get<object>());
			Assert.AreSame(o, locator.Get<object>());
		}

		[Test]
		public void CanRegisterObjectByTypeAndID()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();
			DependencyResolutionLocatorKey key = new DependencyResolutionLocatorKey(typeof(object), "foo");

			locator.Add(key, o);
			Assert.IsNotNull(locator.Get(key));
			Assert.AreSame(o, locator.Get(key));
		}

		[Test]
		public void CanRegisterObjectByStringKey()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("Bar", o);
			Assert.IsNotNull(locator.Get("Bar"));
			Assert.AreSame(o, locator.Get("Bar"));
		}

		[Test]
		public void CanRegisterTwoObjectsWithDifferentKeys()
		{
			object o1 = new object();
			object o2 = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o1);
			locator.Add("foo2", o2);

			Assert.AreSame(o1, locator.Get("foo1"));
			Assert.AreSame(o2, locator.Get("foo2"));
		}

		[Test]
		public void RemovingOneObjectDoesntAffectOtherObjects()
		{
			object o1 = new object();
			object o2 = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o1);
			locator.Add("foo2", o2);

			Assert.IsTrue(locator.Remove("foo1"));
			Assert.AreSame(o2, locator.Get("foo2"));
		}

		[Test]
		public void CanAddSameObjectTwiceWithDifferentKeys()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o);
			locator.Add("foo2", o);

			Assert.AreSame(locator.Get("foo1"), locator.Get("foo2"));
		}

		[Test]
		public void AskingForAnUnregisterdObjectReturnsNull()
		{
			IReadWriteLocator locator = new Locator();

			Assert.IsNull(locator.Get("Foo"));
		}

		[Test]
		public void RetrievingARemovedObjectReturnsNull()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("Foo", o);
			locator.Remove("Foo");

			Assert.IsNull(locator.Get("Foo"));
		}

		[Test]
		public void RegisteringAnObjectWithTwoKeysAndRemovingOneKeyLeavesTheOtherOneInTheLocator()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o);
			locator.Add("foo2", o);
			locator.Remove("foo1");

			Assert.AreSame(o, locator.Get("foo2"));
		}

		[Test]
		public void RemovingANonExistantKeyDoesntThrow()
		{
			IReadWriteLocator locator = new Locator();

			Assert.IsFalse(locator.Remove("Baz"));
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void AddingToSameKeyTwiceThrows()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o);
			locator.Add("foo1", o);
		}

		[Test]
		public void RegistrationDoesNotPreventGarbageCollection()
		{
			IReadWriteLocator locator = new Locator();

			locator.Add("foo", new object());
			GC.Collect();

			Assert.IsNull(locator.Get("foo"));
		}

		[Test]
		public void CanFindOutIfContainsAKey()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo", o);
			Assert.IsTrue(locator.Contains("foo"));
			Assert.IsFalse(locator.Contains("foo2"));
		}

		[Test]
		public void CanEnumerate()
		{
			object o1 = new object();
			object o2 = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o1);
			locator.Add("foo2", o2);

			foreach (KeyValuePair<object, object> kvp in locator)
			{
				Assert.IsNotNull(kvp);
				Assert.IsNotNull(kvp.Key);
				Assert.IsNotNull(kvp.Value);
			}
		}

		[Test]
		public void CountReturnsNumberOfKeysWithLiveValues()
		{
			object o = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o);
			locator.Add("foo2", o);

			Assert.AreEqual(2, locator.Count);

			o = null;
			GC.Collect();

			Assert.AreEqual(0, locator.Count);
		}

		[Test]
		public void GetCanAskParentLocatorForAnObject()
		{
			object o = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);

			rootLocator.Add("Foo", o);
			object result = childLocator.Get("Foo", SearchMode.Up);

			Assert.IsNotNull(result);
			Assert.AreSame(o, result);
		}

		[Test]
		public void TripleNestedLocators()
		{
			object o = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);
			IReadWriteLocator grandchildLocator = new Locator(childLocator);

			rootLocator.Add("bar", o);

			object result = grandchildLocator.Get("bar", SearchMode.Up);

			Assert.IsNotNull(result);
			Assert.AreSame(o, result);
		}

		[Test]
		public void AskingParentStopsAsSoonAsWeFindAMatch()
		{
			object o1 = new object();
			object o2 = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);
			IReadWriteLocator grandchildLocator = new Locator(childLocator);

			rootLocator.Add("foo", o1);
			childLocator.Add("foo", o2);

			object result = grandchildLocator.Get("foo", SearchMode.Up);

			Assert.IsNotNull(result);
			Assert.AreSame(o2, result);
		}

		[Test]
		public void DefaultBehaviorIsAskingParent()
		{
			object o = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);

			rootLocator.Add("fiz", o);

			Assert.IsNotNull(childLocator.Get("fiz"));
		}

		[Test]
		public void CanCallContainsThroughParent()
		{
			object o = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);

			rootLocator.Add("froz", o);

			Assert.IsFalse(childLocator.Contains("froz", SearchMode.Local));
			Assert.IsTrue(childLocator.Contains("froz", SearchMode.Up));
		}

		[Test]
		public void CanFindByPredicate()
		{
			bool wasCalled = false;
			object o1 = new object();
			object o2 = new object();
			IReadWriteLocator locator = new Locator();

			locator.Add("foo1", o1);
			locator.Add("foo2", o2);

			IReadableLocator results;

			results = locator.FindBy(
				delegate(KeyValuePair<object, object> kvp)
				{
					wasCalled = true;
					return kvp.Key.Equals("foo1");
				});

			Assert.IsNotNull(results);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(1, results.Count);
			Assert.AreSame(o1, results.Get("foo1"));
		}

		[Test]
		public void DefaultFindByBehaviorIsAskParent()
		{
			object o = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);

			rootLocator.Add("foo", o);

			IReadableLocator results;

			results = childLocator.FindBy(delegate(KeyValuePair<object, object> kvp)
																			{
																				return true;
																			});

			Assert.AreEqual(1, results.Count);
		}

		[Test]
		public void FindingByPredicateCanClimbTheTree()
		{
			object o = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);

			rootLocator.Add("bar", o);

			IReadableLocator results;

			results = childLocator.FindBy(
				SearchMode.Up,
				delegate(KeyValuePair<object, object> kvp)
				{
					return true;
				});

			Assert.AreEqual(1, results.Count);
			Assert.AreSame(o, results.Get("bar"));
		}

		[Test]
		public void FindingByPredicateCanFindsResultsFromBothParentAndChild()
		{
			object o = new object();
			string s = "Hello world";
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);

			rootLocator.Add("foo", o);
			childLocator.Add("bar", s);

			IReadableLocator results;

			results = childLocator.FindBy(
				SearchMode.Up,
				delegate(KeyValuePair<object, object> kvp)
				{
					return true;
				});

			Assert.AreEqual(2, results.Count);
			Assert.AreSame(o, results.Get("foo"));
			Assert.AreSame(s, results.Get("bar"));
		}

		[Test]
		public void FindingByPredicateReturnsClosestResultsOnDuplicateKey()
		{
			object o1 = new object();
			object o2 = new object();
			IReadWriteLocator rootLocator = new Locator();
			IReadWriteLocator childLocator = new Locator(rootLocator);

			rootLocator.Add("foo", o1);
			childLocator.Add("foo", o2);

			IReadableLocator results;

			results = childLocator.FindBy(
				SearchMode.Up,
				delegate(KeyValuePair<object, object> kvp)
				{
					return true;
				});

			Assert.AreEqual(1, results.Count);
			Assert.AreSame(o2, results.Get("foo"));
		}
	}
}
