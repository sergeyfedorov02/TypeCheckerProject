language core;

extend with #sequencing, #unit-type;

fn main(n : Nat) -> Nat {
  return (fn (n : Nat) { return unit; n })(0)
}