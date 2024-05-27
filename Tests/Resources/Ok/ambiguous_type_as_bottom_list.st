language core;

extend with #structural-subtyping, #ambiguous-type-as-bottom, #lists, #top-type;

fn main(n : Nat) -> Top {
  return List::head([])
}
