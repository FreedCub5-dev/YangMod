// YangModExtraStubs_Fixed.cs
// Extra helpers and non-shadowing EntityState stubs.

using System;
using UnityEngine;
using RoR2;
using EntityStates;

namespace YangMod.Modules
{
    // Skins helper stub
}

namespace YangMod.Modules.Characters
{
    public static class Characters
    {
        public static void Init() { }
    }
}

// IMPORTANT: Do NOT create namespace YangMod.EntityStates here - that would shadow the real EntityStates namespace.
// If you need small state stubs, reference the real EntityStates namespace types from the game assembly.
