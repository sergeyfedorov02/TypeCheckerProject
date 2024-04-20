language core;

extend with #top-type, #sum-types, #unit-type, #type-cast-patterns, #lists, #structural-patterns, #structural-subtyping, #ambiguous-type-as-bottom;

fn toNat(x : Top) -> Nat + Unit {
  return match x {
    n cast as Nat => inl(n)
  | b cast as Bool => inl(if b then succ(0) else 0)
  | [x] cast as [Top] => toNat(x)
  | something => inr(unit)
  }
}

fn main(n : Nat) -> Nat {
  return n
}
