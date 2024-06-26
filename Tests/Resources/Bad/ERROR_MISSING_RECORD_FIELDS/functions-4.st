language core;
extend with
  #structural-subtyping,
  #records;

fn main(
  x : fn(fn({ a : Nat }) -> Nat) -> Nat
) -> fn(fn({ a : Nat, b : Nat }) -> Nat) -> Nat {
  return x
}