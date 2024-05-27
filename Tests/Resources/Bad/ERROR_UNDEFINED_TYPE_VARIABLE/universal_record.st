language core;

extend with #universal-types, #records;

generic fn do[T](x : { a : C, b : Nat }) -> T {
	return x.a
}

fn main(x : Nat) -> Nat {
	return do[Nat]({a = x, b = 0})
}