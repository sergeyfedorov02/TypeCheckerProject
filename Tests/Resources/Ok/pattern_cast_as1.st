language core;

extend with #top-type, #type-cast-patterns, #structural-subtyping;

fn toNat(x : Top) -> Nat {
  return match x {
    n cast as Nat => n
  | b cast as Bool => if b then succ(0) else 0
  | something => 0
  }
}

fn main(n : Nat) -> Nat {
  return n
}
