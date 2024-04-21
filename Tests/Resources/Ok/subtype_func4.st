language core;

extend with #records,
            #structural-subtyping,
						#nullary-variant-labels;

fn foo(n : Nat) -> (fn({a : Nat}) -> {x : Bool, y : Bool}) {
	return fn(x : {a : Nat}) { return {x = true, y = false} }
}

fn main(n : Nat) -> (fn({a : Nat, b : Nat}) -> {x : Bool}) {
  return foo(n)
}