language core;

extend with #fixpoint-combinator, #structural-patterns, #type-cast-patterns;

fn main(n : Nat) -> Nat {
  return match(n) {
      0 cast as Nat => n
      | succ(x) => n
  }
}