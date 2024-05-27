language core;

extend with #type-reconstruction, #lists;

fn do(list : auto) -> auto {
  return cons(succ(0), list)
}

fn main(arg : Nat) -> [Nat] {
  return do([true, false])
}