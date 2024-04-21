language core;

extend with #exceptions, #structural-subtyping;

fn main(n : Nat) -> Nat {
  return (fn (n : Nat) {return throw(0)}) (0)
}