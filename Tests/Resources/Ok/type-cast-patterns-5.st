language core;
extend with #type-cast-patterns, #structural-subtyping, #top-type;

fn main(x : Nat) -> Top {
  return match x {
      y cast as Nat => 0
    | _ => false
  }
}