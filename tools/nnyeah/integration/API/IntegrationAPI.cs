﻿using System;

namespace IntegrationAPI
{
    public class NIntArgs
    {
        public NIntArgs (nint value) { Value = value; }
        public nint Value { get; }
    }

    public class NUIntArgs
    {
        public NUIntArgs (nuint value) { Value = value; }
        public nuint Value { get; }
    }

    public class NFloatArgs
    {
        public NFloatArgs (nfloat value) { Value = value; }
        public nfloat Value { get; }
    }

    public delegate void EventWithNInt(object sender, NIntArgs e);
    public delegate void EventWithNUInt(object sender, NUIntArgs e);
    public delegate void EventWithNFloat(object sender, NFloatArgs e);

    public class NIntAPI
    {
        public NIntAPI ()
        {
        }

        public nint EchoMethod (nint x) => x;
        public nint Prop { get; set; }
        public nint Field;
#pragma warning disable CS0067 // The event 'NIntAPI.Event' is never used
        public event EventWithNInt Event;
#pragma warning disable CS0067 // The event 'NIntAPI.Event' is never used
    }

    public class NUIntAPI
    {
        public NUIntAPI ()
        {
        }

        public nuint EchoMethod (nuint x) => x;
        public nuint Prop { get; set; }
        public nuint Field;
#pragma warning disable CS0067 // The event 'NUIntAPI.Event' is never used
        public event EventWithNUInt Event;
#pragma warning disable CS0067 // The event 'NUIntAPI.Event' is never used
    }

    public class NFloatAPI
    {
        public NFloatAPI ()
        {
        }

        public nfloat EchoMethod (nfloat x) => x;
        public nfloat Prop { get; set; }
        public nfloat Field;
#pragma warning disable CS0067 // The event 'NFloatAPI.Event' is never used
        public event EventWithNFloat Event;
#pragma warning disable CS0067 // The event 'NFloatAPI.Event' is never used
    }
}