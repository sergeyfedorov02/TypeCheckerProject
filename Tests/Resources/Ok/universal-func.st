language core;

extend with #universal-types, #multiparameter-functions;

generic fn apply[T](x : fn(T) -> T, y : T) -> T {
	return x(y)
}

generic fn do[T](x : T) -> fn(T) -> T {
	return fn(y : T) {
    return x
  }
}

fn main(x : Nat) -> Nat {
	return apply[Nat](do[Nat](x), x)
}