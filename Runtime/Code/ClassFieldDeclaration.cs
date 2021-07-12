﻿using System;
using Unity.VisualScripting;

namespace Bolt.Addons.Community.Code
{
    [Serializable]
    [Inspectable]
    public sealed class ClassFieldDeclaration : FieldDeclaration
    {
        public object defaultValue;
    }
}
