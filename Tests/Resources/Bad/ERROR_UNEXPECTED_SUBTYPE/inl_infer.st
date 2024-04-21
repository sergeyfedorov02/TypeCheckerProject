language core;
extend with #references, #sum-types, #structural-subtyping, #lists, #ambiguous-type-as-bottom;

fn main(n : Nat) -> Nat {
  return (fn (n : Nat) { return inl(0) }) (0)
}