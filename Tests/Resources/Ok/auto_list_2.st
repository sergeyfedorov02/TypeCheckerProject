language core;

extend with #type-reconstruction, #lists;

fn first_or_default(list : auto) -> auto {
  return cons(succ(0), list)
}

fn main(arg : Nat) -> [Nat] {
  return first_or_default([0, 0, 0])
}
