language core;
extend with
  #structural-subtyping,
  #records;

fn main(
  x : fn(Nat) -> { a : Nat }
) -> fn(Nat) -> { a : Nat, b : Nat } {
  return x
}