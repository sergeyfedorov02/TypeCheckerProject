language core;

extend with #structural-patterns, #type-cast-patterns, #lists, #sum-types, #multiparameter-functions,  #tuples, #records, #pattern-ascriptions, #structural-subtyping;

fn vectorSum(v1 : [Nat] + [Nat]) -> [Nat] + [Nat] {
  return
   match v1 {
   inl([x]) => v1
   | inl(x) => v1
     | inr(x) => v1
    }
}

fn main(n : Nat) -> Nat {
  return n
}
