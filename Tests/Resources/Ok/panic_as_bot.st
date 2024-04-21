language core;
extend with #references, #panic, #bottom-type, #structural-subtyping, #lists, #ambiguous-type-as-bottom;

fn main(n : Nat) -> Bot {
  return (fn (n : Nat) { return panic! }) (0)
}
