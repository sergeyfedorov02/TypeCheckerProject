language core;
extend with #references, #ambiguous-type-as-bottom, #structural-subtyping, #lists;

fn main(n : Nat) -> &Nat {
  return (fn (n : Nat) { return [] }) (0)
}