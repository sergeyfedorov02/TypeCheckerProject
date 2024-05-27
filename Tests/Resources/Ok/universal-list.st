language core;

extend with #universal-types, #lists;

generic fn do[T](x : [T]) -> T {
	return List::head(x)
}

fn main(x : Nat) -> Nat {
	return do[Nat]([x, x, x])
}