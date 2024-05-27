language core;

extend with #type-reconstruction, #lists;

fn first_or_default(list : auto) -> auto {
  return Nat::iszero(List::head(list))
}

fn main(arg : Nat) -> Bool {
  return first_or_default([0, 0])
}
