



namespace SpaceGame {

  using System;

  [Flags]
  public enum PlanetType : uint {
    None = 0,

    Dwarf = 1 << 0,
    Giant = 1 << 1,

    /// <summary> An extrasolar planet that orbits close to its parent star. Most Chthonian planets are expected to be gas giants that had their atmospheres stripped away, leaving their cores. </summary>
    Chthonian = 1 << 2,
    /// <summary> A theoretical terrestrial planet that could form if protoplanetary discs are carbon-rich and oxygen-poor. </summary>
    Carbon = 1 << 3,
    /// <summary> A hypothetical planet where all of its surface is urbanized. </summary>
    City = 1 << 4,
    /// <summary> A theoretical planet that has undergone planetary differentiation but has no metallic core. Not to be confused with the Hollow Earth concept. </summary>
    Coreless = 1 << 5,
    /// <summary> A theoretical terrestrial planet with very little water. </summary>
    Desert = 1 << 6,
    /// <summary> A planet composed primarily of hydrogen and helium. </summary>
    Gas = 1 << 7,
    /// <summary> A theoretical planet that may form via mass loss from a low-mass white dwarf. </summary>
    Helium = 1 << 8,
    /// <summary> A giant planet composed mainly of 'ices'—volatile substances heavier than hydrogen and helium, such as water, methane, and ammonia—as opposed to 'gas' (hydrogen and helium). </summary>
    IceGiant = Ice | Giant,
    /// <summary> A theoretical planet with an icy surface and consists of a global cryosphere. </summary>
    Ice = 1 << 9,
    /// <summary> A theoretical planet that consists primarily of an iron-rich core with little or no mantle. </summary>
    Iron = 1 << 10,
    /// <summary> A theoretical terrestrial planet with a surface mostly or entirely covered by molten lava. </summary>
    Lava = 1 << 11,
    /// <summary> A theoretical planet which has a substantial fraction of its mass made of water. </summary>
    Ocean = 1 << 12,
    /// <summary> A large planetary embryo that originates within protoplanetary discs and has undergone internal melting to produce differentiated interiors. </summary>
    Protoplanet = 1 << 13,
    /// <summary> Also known as a hot Saturn. A gas giant with a large radius and very low density which is similar to or lower than Saturn's. </summary>
    Puffy = 1 << 14,
    /// <summary> A terrestrial planet that is composed primarily of silicate rocks. All four inner planets in our Solar System are silicon-based. </summary>
    Silicate = 1 << 15,
    /// <summary> Also known as a telluric planet or rocky planet. A planet that is composed primarily of carbonaceous or silicate rocks or metals. </summary>
    Terrestrial = 1 << 16,

    Any = uint.MaxValue >> -16,
  }

}