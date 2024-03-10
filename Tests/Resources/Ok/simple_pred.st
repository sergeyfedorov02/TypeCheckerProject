language core;

extend with #predecessor;

fn main(n : Nat) -> Nat {
  return Nat::pred(succ(0));
}
