language core;
extend with
  #structural-subtyping,
  #top-type,
  #sum-types,
  #records;

fn main(x : { a : Nat, b : Nat } + Nat) -> { a : Top } + Nat {
  return x
}