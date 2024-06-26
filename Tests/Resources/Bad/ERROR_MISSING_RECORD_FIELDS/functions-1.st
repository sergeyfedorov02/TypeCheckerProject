language core;
extend with
  #structural-subtyping,
  #records;

fn main(
  x : fn({ a : Nat, b : Nat }) -> Nat
) -> fn({ a : Nat }) -> Nat {
  return x
}