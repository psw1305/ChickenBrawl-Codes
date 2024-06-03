using System;
using UnityEngine;

namespace CKB.Core
{
    /// <summary>
    /// Owner and its action. Used to unsub zero actions with already destroyed owners
    /// </summary>
    public class Subscriber
    {
        public GameObject owner;
        public Action action;
    }
}