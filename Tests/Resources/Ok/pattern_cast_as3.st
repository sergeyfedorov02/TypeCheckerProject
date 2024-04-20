language core;

extend with #structural-patterns, #type-cast-patterns, #natural-literals, #multiparameter-functions, #arithmetic-operators, #tuples, #records, #pattern-ascriptions, #structural-subtyping;

fn vectorSum(v1 : {x : Nat}, v2 : {x : Nat}) -> {x : Nat} {
  return
   match {v1, v2} {
      {{x = x1, y = y1}, {x = x2, y = y2}} cast as {{x : Nat, y : Nat}, {x : Nat, y : Nat}} => {x = x1 + x2, y = y1 + y2}
    | {{x = x1}, {x = x2}} => {x = x1 + x2}
    }
}

fn main(n : Nat) -> Nat {
  return n
}
