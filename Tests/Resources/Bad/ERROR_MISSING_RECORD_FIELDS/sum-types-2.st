language core;
extend with
  #structural-subtyping,
  #top-type,
  #sum-types,
  #records;

fn main(x : { a : Nat } + Nat) -> { a : Top, b : Nat } + Nat {
  return x
}