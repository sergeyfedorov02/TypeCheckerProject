language core;

extend with #ambiguous-type-as-bottom, #structural-subtyping, #sum-types;

fn main(n : Bool) -> Bool + Nat {
  return (fn (x : Bool) {
    return inl(x) // в этом месте выводится тип-сумма Bot + Nat
  })(n)
}
